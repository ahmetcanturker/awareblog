import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { Archive } from '../contract/dto/Archive';
import { getArchives, GetArchivesResponse } from '../services/ArchiveService';
import { Nullable } from '../util/Types';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ArchiveState {
    loadingArchives: boolean;
    archives: Nullable<Archive[]>;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestArchivesAction {
    type: 'REQUEST_ARCHIVES';
}

interface ReceiveArchivesAction {
    type: 'RECEIVE_ARCHIVES';
    response: GetArchivesResponse;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestArchivesAction | ReceiveArchivesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestArchives: (): AppThunkAction<KnownAction> => async (dispatch, getState) => {
        const appState = getState();

        if (appState && appState.archive && !appState.archive.loadingArchives) {
            dispatch({ type: 'REQUEST_ARCHIVES' });

            let response = await getArchives();

            dispatch({ type: 'RECEIVE_ARCHIVES', response: response });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ArchiveState = { archives: null, loadingArchives: false };

export const reducer: Reducer<ArchiveState> = (state: ArchiveState | undefined, incomingAction: Action): ArchiveState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ARCHIVES':
            return {
                archives: state.archives,
                loadingArchives: true
            };
        case 'RECEIVE_ARCHIVES':
            return {
                archives: action.response.success ? action.response.data : state.archives,
                loadingArchives: false
            };
    }

    return state;
};
