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
});

type GridPostProps = {
    post: BlogPost;
};

export default function GridPost(props: GridPostProps) {
    const classes = useStyles();
    const { post } = props;

    return (
        <Grid item xs={12} md={6}>
            <CardActionArea component={RouterLink} to="test2">
                <Card className={classes.card}>
                    <div className={classes.cardDetails}>
                        <CardContent>
                            <Typography component="h2" variant="h5">
                                {post.title}
                            </Typography>
                            <Typography variant="subtitle1" color="textSecondary">
                                {dateText(post.date)}
                            </Typography>
                            <Typography variant="subtitle1" paragraph>
                                {post.summary}
                            </Typography>
                            <Typography variant="subtitle1" color="primary">
                                Devamını oku
                            </Typography>
                        </CardContent>
                    </div>
                    <Hidden xsDown>
                        <CardMedia className={classes.cardMedia} image={post.image.url} title={post.image.description} />
                    </Hidden>
                </Card>
            </CardActionArea>
        </Grid>
    );
}