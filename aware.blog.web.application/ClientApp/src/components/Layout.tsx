import React, { PropsWithChildren } from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Container from '@material-ui/core/Container';
import Header from '../components/Header';
import Footer from '../components/Footer';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    container: {
        paddingTop: theme.spacing(4)
    }
}));

type LayoutProps = PropsWithChildren<{

}>;

export default function Layout(props: LayoutProps) {
    const classes = useStyles();

    return (
        <React.Fragment>
            <CssBaseline />
            <Header title="Aware Blog" />
            <Container maxWidth="lg" className={classes.container}>
                <main>
                    {props.children}
                </main>
            </Container>
            <Footer title="Aware Blog" description="Blog olsun diye..." />
        </React.Fragment>
    );
}