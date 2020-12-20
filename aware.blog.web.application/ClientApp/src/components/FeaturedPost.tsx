import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import { BlogPost } from '../contract/dto/BlogPost';
import Skeleton from '@material-ui/lab/Skeleton';
import { Nullable } from '../util/Types';
import { AppRoutes } from '../util/Routes';

const useStyles = makeStyles((theme) => ({
    mainFeaturedPost: {
        position: 'relative',
        backgroundColor: theme.palette.grey[800],
        color: theme.palette.common.white,
        marginBottom: theme.spacing(4),
        backgroundSize: 'cover',
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'center',
    },
    overlay: {
        position: 'absolute',
        top: 0,
        bottom: 0,
        right: 0,
        left: 0,
        backgroundColor: 'rgba(0,0,0,.3)',
    },
    mainFeaturedPostContent: {
        position: 'relative',
        padding: theme.spacing(3),
        [theme.breakpoints.up('md')]: {
            padding: theme.spacing(6),
            paddingRight: 0,
        },
    },
}));

type FeaturedPostProps = {
    post: Nullable<BlogPost>;
};

export default function FeaturedPost(props: FeaturedPostProps) {
    const classes = useStyles();
    const { post } = props;

    return (
        <Paper className={classes.mainFeaturedPost} style={{ backgroundImage: `url(${post ? post.image.url : ''})` }}>
            {/* Increase the priority of the hero background image */}
            {post && <img style={{ display: 'none' }} src={post.image.url} alt={post.image.description} />}
            <div className={classes.overlay} />
            <Grid container>
                <Grid item md={6}>
                    <div className={classes.mainFeaturedPostContent}>
                        <Typography component="h1" variant="h3" color="inherit" gutterBottom>
                            {post && post.title}
                            {!post && <Skeleton animation="wave" />}
                        </Typography>
                        <Typography variant="h5" color="inherit" paragraph>
                            {post && post.summary}
                            {!post && <Skeleton animation="wave" />}
                        </Typography>
                        <Link component={RouterLink} to={post ? AppRoutes.blogPost(post.uri) : '#'} variant="subtitle1">
                            {post && 'Devamını oku'}
                            {!post && <Skeleton animation="wave" />}
                        </Link>
                    </div>
                </Grid>
            </Grid>
        </Paper>
    );
}