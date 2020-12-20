import { Entity } from './base/Entity';
import { int } from '../../util/Types';

export interface Image extends Entity<int> {
    url: string;
    description: string;
}