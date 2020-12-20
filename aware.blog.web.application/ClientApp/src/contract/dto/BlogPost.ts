import { Image } from './Image';
import { DateTime, int, NullableString } from '../../util/Types';
import { Entity } from './base/Entity';
import { User } from './User';
import { BlogPostTag } from './BlogPostTag';

export interface BlogPost extends Entity<int> {
    imageId: int;
    authorId: int;
    uri: string;
    title: string;
    description: NullableString;
    summary: NullableString;
    contentMarkdown: string;

    image: Image;
    author: User;
    tags: BlogPostTag[];
}