import { int } from './Types';
import { buildQueryParameterString } from './HttpUtilities';

const apiBaseUri = `/api`;

const paginationQueryString = (pageIndex: int, pageLength: int) =>
    buildQueryParameterString({
        pageIndex: pageIndex,
        pageLength: pageLength
    });

export const ApiRoutes = {
    archives: {
        get: () => `${apiBaseUri}/archives`,
        getByYearAndMonth: (year: int, month: int, pageIndex: int, pageLength: int) =>
            `/api/archives/${year}/${month}?${paginationQueryString(pageIndex, pageLength)}`
    },
    blogPosts: {
        get: (pageIndex: int, pageLength: int) =>
            `/api/blog-posts?${paginationQueryString(pageIndex, pageLength)}`,
        getByUri: (uri: string) => `/api/blog-posts/${encodeURI(uri)}`,
        getFeatured: () => `/api/blog-posts/featured`
    }
};

export const AppRoutes = {
    home: (pageIndex?: int) => `/?${buildQueryParameterString({ pageIndex: pageIndex })}`,
    blogPost: (uri: string) => `/makaleler/${uri}`,
    archive: (year: int, month: int, pageIndex?: int) => `/arsiv/${year}/${month}?${buildQueryParameterString({ pageIndex: pageIndex })}`
};