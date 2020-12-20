import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import { Link as RouterLink } from 'react-router-dom';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Hidden from '@material-ui/core/Hidden';
import { BlogPost } from '../contract/dto/BlogPost';
import { dateText } from '../util/TextUtilities';
import { Nullable } from '../util/Types';
import Skeleton from '@material-ui/lab/Skeleton';
import { AppRoutes } from '../util/Routes';

const useStyles = makeStyles({
    card: {
        display: 'flex',
    },
    cardDetails: {
        flex: 1,
    },
    cardMedia: {
        width: 160,
    },
    media: {
        width: 160,
        height: 190,
    }
});

type GridPostProps = {
    post: Nullable<BlogPost>;
};

export default function GridPost(props: GridPostProps) {
    const classes = useStyles();
    const { post } = props;

    return (
        <Grid item xs={12}>
            <CardActionArea component={RouterLink} to={post ? AppRoutes.blogPost(post.uri) : '#'}>
                <Card className={classes.card}>
                    <div className={classes.cardDetails}>
                        <CardContent>
                            <Typography component="h2" variant="h5">
                                {post && post.title}
                                {!post && <Skeleton animation="wave" />}
                            </Typography>
                            <Typography variant="subtitle1" color="textSecondary">
                                {post && dateText(post.createdTime)}
                                {!post && <Skeleton animation="wave" />}
                            </Typography>
                            <Typography variant="subtitle1" paragraph>
                                {post && post.summary}
                                {!post && <Skeleton animation="wave" />}
                            </Typography>
                        </CardContent>
                    </div>
                    <Hidden xsDown>
                        {post && <CardMedia className={classes.cardMedia} image={post.image.url} title={post.image.description} />}
                        {!post && <Skeleton animation="wave" variant="rect" className={classes.media} />}
                    </Hidden>
                </Card>
            </CardActionArea>
        </Grid>
    );
}