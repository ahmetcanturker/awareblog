import React from 'react';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import Chip from '@material-ui/core/Chip';
import FaceIcon from '@material-ui/icons/Face';
import DoneIcon from '@material-ui/icons/Done';
import { Link as RouterLink } from 'react-router-dom';
import { Tag } from '../contract/dto/Tag';
import { Nullable } from '../util/Types';
import Skeleton from '@material-ui/lab/Skeleton';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            padding: theme.spacing(2),
            display: 'flex',
            justifyContent: 'flex-end',
            flexWrap: 'wrap',
            '& > *': {
                margin: theme.spacing(0.5),
            },
        },
    }),
);

type TagChipProps = {
    tag: Nullable<Tag>;
};

const TagChip = (props: TagChipProps) => (
    <React.Fragment>
        {!props.tag && (<Skeleton animation="wave" height={25} width={100} />)}
        {props.tag && (<Chip label={props.tag.name} component={RouterLink} to={'#'} clickable disabled />)}
    </React.Fragment>
);

type TagsProps = {
    tags: Nullable<Tag>[];
};

export default function Tags(props: TagsProps) {
    const classes = useStyles();
    const { tags } = props;

    return (
        <div className={classes.root}>
            {tags.map((tag, i) => (
                <TagChip key={i} tag={tag} />
            ))}
        </div>
    );
}