import React, { PropsWithChildren } from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Container from '@material-ui/core/Container';
import Header from '../components/Header';
import Footer from '../components/Footer';

type LayoutProps = PropsWithChildren<{

}>;

export default function Layout(props: LayoutProps) {
    return (
        <React.Fragment>
            <CssBaseline />
            <Container maxWidth="lg">
                <Header title="Aware Blog" />
                <main>
                    {props.children}
                </main>
            </Container>
            <Footer title="Aware Blog" description="Blog olsun diye..." />
        </React.Fragment>
    );
}