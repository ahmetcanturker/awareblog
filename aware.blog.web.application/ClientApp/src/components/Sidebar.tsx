import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import { Nullable } from '../util/Types';
import { Archive } from '../contract/dto/Archive';
import Skeleton from '@material-ui/lab/Skeleton';
import { monthText } from '../util/TextUtilities';
import Chip from '@material-ui/core/Chip';
import GitHubIcon from '@material-ui/icons/GitHub';
import TwitterIcon from '@material-ui/icons/Twitter';
import { AppRoutes } from '../util/Routes';

const useStyles = makeStyles((theme) => ({
    sidebarAboutBox: {
        padding: theme.spacing(2),
        backgroundColor: theme.palette.grey[200],
    },
    sidebarSection: {
        marginTop: theme.spacing(3),
    },
    chipContainer: {
        display: 'flex',
        marginTop: theme.spacing(1)
    },
    chip: {
        marginLeft: 'auto'
    }
}));

export type SidebarProps = {
    archives: Nullable<Nullable<Archive>[]>;
    description: string;
    social: { icon: React.ComponentType, name: string; url: string; }[];
    title: string;
};

export const DefaultSidebarOptions: Pick<SidebarProps, 'description' | 'title' | 'social'> = {
    title: 'Hakkında',
    description: 'Blog geliştirme serisi üzerine kurulu blogdur. Makale makale okuyucularla birlikte bir blog inşa etmeyi amaçlar.',
    social: [
        { name: 'GitHub', icon: GitHubIcon, url: 'https://github.com/ahmetcanturker/awareblog' },
        { name: 'Twitter', icon: TwitterIcon, url: 'https://twitter.com/ahmetcanturker' }
    ]
};

function ArchiveLink(props: { archive: Nullable<Archive> }) {
    const classes = useStyles();

    return (
        <React.Fragment>
            {!props.archive && (
                <Typography display='block' variant='body1'>
                    <Skeleton animation="wave" height={25} width={100} />
                </Typography>
            )}
            {props.archive && (
                // <Badge badgeContent={props.archive.count} color="primary">
                <Link component={RouterLink} variant="body1" to={AppRoutes.archive(props.archive.year, props.archive.month)} className={classes.chipContainer}>
                    {monthText(props.archive.month)} {props.archive.year}
                    <Chip label={props.archive.count} color="primary" size="small" className={classes.chip} />
                </Link>
                // </Badge>
            )}
        </React.Fragment>
    )
};

export default function Sidebar(props: SidebarProps) {
    const classes = useStyles();
    const { archives, description, social, title } = props;

    return (
        <Grid item xs={12} md={4}>
            <Paper elevation={0} className={classes.sidebarAboutBox}>
                <Typography variant="h6" gutterBottom>
                    {title}
                </Typography>
                <Typography>{description}</Typography>
            </Paper>
            <Typography variant="h6" gutterBottom className={classes.sidebarSection}>
                Arşiv
            </Typography>
            {archives !== null && archives.map((archive, i) => (
                <ArchiveLink key={i} archive={archive} />
            ))}
            <Typography variant="h6" gutterBottom className={classes.sidebarSection}>
                Mecralar
            </Typography>
            {social.map((network, i) => (
                <Link display="block" variant="body1" href={network.url} key={i} target='_blank'>
                    <Grid container direction="row" spacing={1} alignItems="center">
                        <Grid item>
                            <network.icon />
                        </Grid>
                        <Grid item>{network.name}</Grid>
                    </Grid>
                </Link>
            ))}
        </Grid>
    );
}

Sidebar.propTypes = {
    archives: PropTypes.array,
    description: PropTypes.string,
    social: PropTypes.array,
    title: PropTypes.string,
};