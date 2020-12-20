import React from 'react';
import { withStyles, WithStyles, createStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import FeaturedPost from '../components/FeaturedPost';
import Main from '../components/Main';
import Sidebar, { SidebarProps, DefaultSidebarOptions } from '../components/Sidebar';
import { getBlogPosts, GetBlogPostsResponse } from '../services/BlogPostService';
import { Nullable, int } from '../util/Types';
import Button from '@material-ui/core/Button';
import { BlogPost } from '../contract/dto/BlogPost';
import { RouteComponentProps } from 'react-router';
import { Link as RouterLink } from 'react-router-dom';
import { parseQueryString, buildQueryParameterString } from '../util/HttpUtilities';
import { ApplicationState } from '../store';
import * as ArchiveStore from '../store/Archive';
import * as BlogStore from '../store/Blog';
import { connect } from 'react-redux';
import { AppRoutes } from '../util/Routes';

const styles = (theme: Theme) => createStyles({
    container: {
        display: 'flex',
        paddingTop: theme.spacing(3)
    },
    mainGrid: {
        marginTop: theme.spacing(3),
        marginBottom: theme.spacing(3)
    },
    previousButton: {

    },
    nextButton: {
        marginLeft: 'auto'
    },
    divider: {
        marginBottom: theme.spacing(1)
    }
});

type HomeProps =
    ArchiveStore.ArchiveState &
    BlogStore.BlogState &
    typeof ArchiveStore.actionCreators &
    typeof BlogStore.actionCreators &
    WithStyles<typeof styles> &
    RouteComponentProps & {

    };

type HomeState = {
    pageIndex: Nullable<int>;
    pageLength: int;
    loading: boolean;
    response: Nullable<GetBlogPostsResponse>;
    blogPosts: BlogPost[];
};

class Home extends React.Component<HomeProps, HomeState> {
    constructor(props: HomeProps) {
        super(props);

        this.state = {
            pageIndex: null,
            pageLength: 10,
            loading: false,
            response: null,
            blogPosts: []
        };
    }

    async componentDidMount() {
        await this.fetchArchives();
        await this.fetchFeaturedBlogPost();
        await this.fetchData();
    }

    async componentDidUpdate() {
        await this.fetchData();
    }

    async fetchArchives() {
        if (this.props.archives !== null) {
            return;
        }

        this.props.requestArchives();
    }

    async fetchFeaturedBlogPost() {
        if (this.props.featuredPost !== null) {
            return;
        }

        this.props.requestFeaturedBlogPost();
    }

    async fetchData() {
        if (this.state.loading) {
            return;
        }

        let queryParameters = parseQueryString<{ pageIndex: number; }>(this.props.location.search);

        let pageIndex = !isNaN(queryParameters.pageIndex) ? queryParameters.pageIndex : 0;

        if (pageIndex === this.state.pageIndex) {
            return;
        }

        this.setState({
            loading: true,
            response: null,
            pageIndex: pageIndex
        });

        let response = await getBlogPosts(pageIndex, this.state.pageLength);

        this.setState({
            loading: false,
            response: response,
            blogPosts: response.success ? response.data : []
        });
    }

    public render() {
        const { classes } = this.props;

        return (
            <React.Fragment>
                <FeaturedPost post={!this.props.loadingFeaturedPost ? this.props.featuredPost : null} />
                <Grid container spacing={5} className={classes.mainGrid}>
                    <Grid item xs={12}>
                        <Typography variant="h6" gutterBottom>
                            Awareblog
                        </Typography>
                        <Divider className={classes.divider} />
                    </Grid>
                    <Main posts={!this.state.loading ? this.state.blogPosts : [null, null, null]}>
                        {!this.state.loading && this.state.response && this.state.response.success && (
                            <div className={classes.container}>
                                {((this.state.pageIndex || 0) > 0) && (
                                    <Button
                                        color="primary"
                                        className={classes.previousButton}
                                        component={RouterLink}
                                        to={AppRoutes.home((this.state.pageIndex || 0) - 1)}
                                    >
                                        ÖNCEKİ SAYFA
                                    </Button>
                                )}
                                {(((this.state.pageIndex || 0) + 1) * this.state.pageLength < this.state.response.totalCount) && (
                                    <Button
                                        color="primary"
                                        className={classes.nextButton}
                                        component={RouterLink}
                                        to={AppRoutes.home((this.state.pageIndex || 0) + 1)}
                                    >
                                        SONRAKİ SAYFA
                                    </Button>
                                )}
                            </div>
                        )}
                    </Main>
                    <Sidebar
                        title={DefaultSidebarOptions.title}
                        description={DefaultSidebarOptions.description}
                        archives={!this.props.loadingArchives ? this.props.archives : [null, null]}
                        social={DefaultSidebarOptions.social}
                    />
                </Grid>
            </React.Fragment>
        );
    }
}

export default connect(
    (state: ApplicationState) => ({ ...state.archive, ...state.blog }),
    { ...ArchiveStore.actionCreators, ...BlogStore.actionCreators }
)(withStyles(styles)(Home) as any);