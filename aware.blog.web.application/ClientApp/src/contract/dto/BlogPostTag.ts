import { Entity } from './base/Entity';
import { int } from '../../util/Types';
import { BlogPost } from './BlogPost';
import { Tag } from './Tag';

export interface BlogPostTag extends Entity<int> {
    blogPostId: int;
    tagId: int;

    blogPost: BlogPost;
    tag: Tag;
}