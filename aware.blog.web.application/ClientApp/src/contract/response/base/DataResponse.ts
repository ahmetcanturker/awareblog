import { Response } from "./Response";

export interface DataResponse<T> extends Response {
    data: T;
}