import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import Markdown from './Markdown';
import { BlogPost } from '../contract/dto/BlogPost';

const useStyles = makeStyles((theme) => ({
  markdown: {
    ...theme.typography.body2,
    padding: theme.spacing(3, 0),
  },
}));

type MainProps = {
    posts: BlogPost[];
    title: string;
};

export default function Main(props: MainProps) {
  const classes = useStyles();
  const { posts, title } = props;

  return (
    <Grid item xs={12} md={8}>
      <Typography variant="h6" gutterBottom>
        {title}
      </Typography>
      <Divider />
      {posts.map((post, i) => (
        <Markdown className={classes.markdown} key={i}>
          {post.contentMd}
        </Markdown>
      ))}
    </Grid>
  );
}