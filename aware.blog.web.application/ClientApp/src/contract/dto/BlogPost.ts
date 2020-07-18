import { Image } from "./Image";
import { DateTime } from "../../util/Types";

export interface BlogPost {
    date: DateTime;
    title: string;
    summary: string;
    image: Image;
    contentMd: string;
}