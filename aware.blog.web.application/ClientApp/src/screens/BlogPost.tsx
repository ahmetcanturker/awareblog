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
import { connect } from 'react-redux';
import { ApplicationState } from '../store';

const styles = (theme: Theme) => createStyles({
    mainGrid: {
        marginTop: theme.spacing(3),
    },
    markdown: {
        ...theme.typography.body2,
        padding: theme.spacing(3, 0),
    }
});

type BlogPostProps =
    ArchiveStore.ArchiveState &
    typeof ArchiveStore.actionCreators &
    WithStyles<typeof styles> &
    RouteComponentProps<{ uri: string; }> & {

    };

type BlogPostState = {
    loading: boolean;
    response: Nullable<GetBlogPostResponse>;
};

class BlogPost extends React.Component<BlogPostProps, BlogPostState> {
    constructor(props: BlogPostProps) {
        super(props);

        this.state = {
            loading: false,
            response: null
        };
    }

    async componentDidMount() {
        this.setState({
            loading: true,
            response: null
        });

        let response = await getBlogPost(this.props.match.params.uri);

        if (response.success) {
            document.title = response.data.title + ' - Aware Blog';
        }

        this.setState({
            loading: false,
            response: response
        });
    }

    public render() {
        const { classes } = this.props;

        return (
            <React.Fragment>
                <Banner post={(!this.state.loading && this.state.response && this.state.response.success) ? this.state.response.data : null} />
                <Grid container spacing={5} className={classes.mainGrid}>
                    <Grid item xs={12} md={8}>
                        {!this.state.loading ? (
                            <Markdown className={classes.markdown}>
                                {(this.state.response && this.state.response.success) ? this.state.response.data.contentMarkdown : ''}
                            </Markdown>
                        ) : (
                                <React.Fragment>
                                    <Typography variant="h1">
                                        <Skeleton animation="wave" />
                                    </Typography>
                                    <Typography variant="h3">
                                        <Skeleton animation="wave" />
                                    </Typography>
                                    <Typography variant="h2">
                                        <Skeleton animation="wave" />
                                    </Typography>
                                </React.Fragment>
                            )}
                        <Tags
                            tags={!this.state.loading ? ((this.state.response && this.state.response.success) ? this.state.response.data.tags.map(x => x.tag) : []) : [null, null]}
                        />
                    </Grid>
                    <Sidebar
                        title={DefaultSidebarOptions.title}
                        description={DefaultSidebarOptions.description}
                        archives={!this.props.loadingArchives ? this.props.archives : [null, null]}
                        social={DefaultSidebarOptions.social}
                    />
                </Grid>
            </React.Fragment >
        );
    }
}

export default connect(
    (state: ApplicationState) => ({ ...state.archive }),
    { ...ArchiveStore.actionCreators }
)(withStyles(styles)(BlogPost) as any);