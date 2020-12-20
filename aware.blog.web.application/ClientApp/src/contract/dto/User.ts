import { Entity } from './base/Entity';
import { int } from '../../util/Types';
import { VerificationStatus } from '../common/VerificationStatus';
import { BlogPost } from './BlogPost';

export interface User extends Entity<int> {
    username: string;
    emailAddress: string;
    emailAddressVerificationStatus: VerificationStatus;
    blogPosts: BlogPost[];
}