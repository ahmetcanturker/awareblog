import { Error } from "../../base/Error";
import { int } from "../../../util/Types";

export interface Response {
    errors: Error[];
    statusCode: int;
    success: boolean;
}