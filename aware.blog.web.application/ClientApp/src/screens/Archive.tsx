import React from 'react';
import { withStyles, WithStyles, createStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Banner from '../components/Banner';
import Sidebar, { SidebarProps, DefaultSidebarOptions } from '../components/Sidebar';
import { getBlogPost, GetBlogPostResponse } from '../services/BlogPostService';
import { Nullable, int } from '../util/Types';
import { RouteComponentProps } from 'react-router';
import Markdown from '../components/Markdown';
import Skeleton from '@material-ui/lab/Skeleton';
import Typography from '@material-ui/core/Typography';
import Tags from '../components/Tags';
import * as ArchiveStore from '../store/Archive';
import * as BlogStore from '../store/Blog';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { getArchiveBlogPosts, GetArchiveBlogPostsResponse } from '../services/ArchiveService';
import { parseQueryString } from '../util/HttpUtilities';
import { monthText } from '../util/TextUtilities';
import FeaturedPost from '../components/FeaturedPost';
import Divider from '@material-ui/core/Divider';
import Main from '../components/Main';
import { BlogPost } from '../contract/dto/BlogPost';
import { Link as RouterLink } from 'react-router-dom';
import { AppRoutes } from '../util/Routes';
import Button from '@material-ui/core/Button';

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

type ArchiveProps =
    ArchiveStore.ArchiveState &
    BlogStore.BlogState &
    typeof ArchiveStore.actionCreators &
    typeof BlogStore.actionCreators &
    WithStyles<typeof styles> &
    RouteComponentProps<{ year: string; month: string; }> & {

    };

type ArchiveState = {
    pageIndex: Nullable<int>;
    pageLength: int;
    month: Nullable<int>;
    year: Nullable<int>;
    loading: boolean;
    response: Nullable<GetArchiveBlogPostsResponse>;
    blogPosts: BlogPost[];
};

class Archive extends React.Component<ArchiveProps, ArchiveState> {
    constructor(props: ArchiveProps) {
        super(props);

        this.state = {
            pageIndex: null,
            pageLength: 10,
            month: null,
            year: null,
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

        const { year: yearString, month: monthString } = this.props.match.params;

        const year = parseInt(yearString);
        const month = parseInt(monthString);

        if (isNaN(year) || isNaN(month)) {
            return;
        }

        let queryParameters = parseQueryString<{ pageIndex: number; }>(this.props.location.search);

        let pageIndex = !isNaN(queryParameters.pageIndex) ? queryParameters.pageIndex : 0;

        if (year === this.state.year && month === this.state.month && pageIndex === this.state.pageIndex) {
            return;
        }

        this.setState({
            loading: true,
            response: null,
            pageIndex: pageIndex,
            year: year,
            month: month
        });

        let response = await getArchiveBlogPosts(year, month, pageIndex, 10);

        if (response.success) {
            document.title = monthText(month) + ' ' + year + ' Arşiv - Aware Blog';
        }

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
                            {monthText(parseInt(this.props.match.params.month))} {parseInt(this.props.match.params.year)} Arşiv - Awareblog
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
                                        to={AppRoutes.archive(this.state.year || 0, this.state.month || 0, (this.state.pageIndex || 0) - 1)}
                                    >
                                        ÖNCEKİ SAYFA
                                    </Button>
                                )}
                                {(((this.state.pageIndex || 0) + 1) * this.state.pageLength < this.state.response.totalCount) && (
                                    <Button
                                        color="primary"
                                        className={classes.nextButton}
                                        component={RouterLink}
                                        to={AppRoutes.archive(this.state.year || 0, this.state.month || 0, (this.state.pageIndex || 0) + 1)}
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
)(withStyles(styles)(Archive) as any);