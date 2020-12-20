import { Error } from "./Error";

export interface ValidationError extends Error {
    propertyName: string;
}