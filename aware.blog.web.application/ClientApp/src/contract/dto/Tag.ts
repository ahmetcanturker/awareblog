import { Entity } from './base/Entity';
import { int } from '../../util/Types';

export interface Tag extends Entity<int> {
    uri: string;
    name: string;
}