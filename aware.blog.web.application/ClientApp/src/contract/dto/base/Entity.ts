import { DateTime } from '../../../util/Types';

export interface Entity<TKey> {
    id: TKey;
    createdTime: DateTime;
}