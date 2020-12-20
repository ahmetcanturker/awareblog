import { ListResponse } from "./ListResponse";
import { int } from "../../../util/Types";

export interface PaginatedListResponse<T> extends ListResponse<T> {
    totalCount: int;
}