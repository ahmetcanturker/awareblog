import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import CounterBs from './components/Counter';
import FetchDataBs from './components/FetchData';
import Home from './screens/Home';
import { ThemeProvider } from '@material-ui/core';
import { applicationMuiLightTheme } from './util/Theme';

export default () => (
    <ThemeProvider theme={applicationMuiLightTheme}>
        <Layout>
            <Switch>
                <Route exact path='/' component={Home}></Route>
            </Switch>
        </Layout>
    </ThemeProvider>
);