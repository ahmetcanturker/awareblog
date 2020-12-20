import React from 'react';
import ReactMarkdown, { MarkdownProps } from 'markdown-to-jsx';
import { withStyles, WithStyles, Theme } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Link from '@material-ui/core/Link';

const styles = (theme: Theme) => ({
    listItem: {
        marginTop: theme.spacing(1),
    },
});

type liComponentProps = WithStyles<typeof styles>;
const liComponent = ({ classes, ...props }: liComponentProps) => (
    <li className={classes.listItem}>
        <Typography component="span" {...props} />
    </li>
);

const options = {
    overrides: {
        h1: {
            component: Typography,
            props: {
                gutterBottom: true,
                variant: 'h5',
            },
        },
        h2: { component: Typography, props: { gutterBottom: true, variant: 'h6' } },
        h3: { component: Typography, props: { gutterBottom: true, variant: 'subtitle1' } },
        h4: {
            component: Typography,
            props: { gutterBottom: true, variant: 'caption', paragraph: true },
        },
        p: { component: Typography, props: { paragraph: true } },
        a: { component: Link },
        li: {
            component: withStyles(styles)(liComponent),
        },
        img: {
            component: (props: any) => (<img style={{ maxWidth: '100%', height: 'auto' }} {...props} />)
        }
    },
};

type _MarkdownProps = MarkdownProps & {

};

export default function Markdown(props: _MarkdownProps) {
    return <ReactMarkdown options={options} {...props} />;
}