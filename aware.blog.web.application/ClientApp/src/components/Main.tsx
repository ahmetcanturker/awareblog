import React, { PropsWithChildren } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import { BlogPost } from '../contract/dto/BlogPost';
import GridPost from './GridPost';
import { Nullable } from '../util/Types';

const useStyles = makeStyles((theme) => ({
  markdown: {
    ...theme.typography.body2,
    padding: theme.spacing(3, 0),
  }
}));

type MainProps = PropsWithChildren<{
  posts: Nullable<BlogPost>[];
}>;

export default function Main(props: MainProps) {
  const classes = useStyles();
  const { posts } = props;

  return (
    <Grid item xs={12} md={8}>
      <Grid container spacing={4}>
        {posts.map((post, i) => (
          <GridPost key={i} post={post} />
        ))}
      </Grid>
      {props.children}
    </Grid>
  );
}