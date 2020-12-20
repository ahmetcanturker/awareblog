import { int } from '../../util/Types';

export interface Error {
    statusCode: int;
    errorCode: string;
    errorMessage: string;
}