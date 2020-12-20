import { fetchJson } from './BaseService';
import { PaginatedListResponse } from '../contract/response/base/PaginatedListResponse';
import { BlogPost } from '../contract/dto/BlogPost';
import { int } from '../util/Types';
import { buildQueryParameterString } from '../util/HttpUtilities';
import { DataResponse } from '../contract/response/base/DataResponse';
import { ApiRoutes } from '../util/Routes';

export type GetBlogPostsResponse = PaginatedListResponse<BlogPost>;
export async function getBlogPosts(
    pageIndex: int,
    pageLength: int): Promise<GetBlogPostsResponse> {
    return await fetchJson<GetBlogPostsResponse>(ApiRoutes.blogPosts.get(pageIndex, pageLength), {
        method: 'GET'
    });
}

export type GetBlogPostResponse = DataResponse<BlogPost>;
export async function getBlogPost(
    uri: string): Promise<GetBlogPostResponse> {
    return await fetchJson<GetBlogPostResponse>(ApiRoutes.blogPosts.getByUri(uri), {
        method: 'GET'
    });
}

export type GetFeatureBlogPostResponse = DataResponse<BlogPost>;
export async function getFeaturedBlogPost(): Promise<GetFeatureBlogPostResponse> {
    return await fetchJson<GetFeatureBlogPostResponse>(ApiRoutes.blogPosts.getFeatured(), {
        method: 'GET'
    });
}