import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Home from './screens/Home';
import BlogPost from './screens/BlogPost';
import Archive from './screens/Archive';
import { ThemeProvider } from '@material-ui/core';
import { applicationMuiLightTheme } from './util/Theme';

export default () => (
    <ThemeProvider theme={applicationMuiLightTheme}>
        <Layout>
            <Switch>
                <Route exact path='/' component={Home} />
                <Route path='/makaleler/:uri' component={BlogPost} />
                <Route path='/arsiv/:year/:month' component={Archive} />
            </Switch>
        </Layout>
    </ThemeProvider>
);