import { fetchJson } from './BaseService';
import { ListResponse } from '../contract/response/base/ListResponse';
import { Archive } from '../contract/dto/Archive';
import { BlogPost } from '../contract/dto/BlogPost';
import { int } from '../util/Types';
import { ApiRoutes } from '../util/Routes';
import { PaginatedListResponse } from '../contract/response/base/PaginatedListResponse';

export type GetArchivesResponse = ListResponse<Archive>;
export async function getArchives(): Promise<GetArchivesResponse> {
    return await fetchJson<GetArchivesResponse>(ApiRoutes.archives.get(), {
        method: 'GET'
    });
}

export type GetArchiveBlogPostsResponse = PaginatedListResponse<BlogPost>;
export async function getArchiveBlogPosts(
    year: int,
    month: int,
    pageIndex: int,
    pageLength: int
): Promise<GetArchiveBlogPostsResponse> {
    return await fetchJson<GetArchiveBlogPostsResponse>(ApiRoutes.archives.getByYearAndMonth(year, month, pageIndex, pageLength), {
        method: 'GET'
    });
}