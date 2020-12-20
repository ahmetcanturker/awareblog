import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { Archive } from '../contract/dto/Archive';
import { getFeaturedBlogPost, GetFeatureBlogPostResponse } from '../services/BlogPostService';
import { Nullable } from '../util/Types';
import { BlogPost } from '../contract/dto/BlogPost';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface BlogState {
    loadingFeaturedPost: boolean;
    featuredPost: Nullable<BlogPost>;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestFeaturedBlogPosAction {
    type: 'REQUEST_FEATURED_BLOG_POST';
}

interface ReceiveFeaturedBlogPosAction {
    type: 'RECEIVE_FEATURED_BLOG_POST';
    response: GetFeatureBlogPostResponse;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestFeaturedBlogPosAction | ReceiveFeaturedBlogPosAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestFeaturedBlogPost: (): AppThunkAction<KnownAction> => async (dispatch, getState) => {
        const appState = getState();

        if (appState && appState.blog && !appState.blog.loadingFeaturedPost) {
            dispatch({ type: 'REQUEST_FEATURED_BLOG_POST' });

            let response = await getFeaturedBlogPost();

            dispatch({ type: 'RECEIVE_FEATURED_BLOG_POST', response: response });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: BlogState = { featuredPost: null, loadingFeaturedPost: false };

export const reducer: Reducer<BlogState> = (state: BlogState | undefined, incomingAction: Action): BlogState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_FEATURED_BLOG_POST':
            return {
                featuredPost: state.featuredPost,
                loadingFeaturedPost: true
            };
        case 'RECEIVE_FEATURED_BLOG_POST':
            return {
                featuredPost: action.response.success ? action.response.data : state.featuredPost,
                loadingFeaturedPost: false
            };
    }

    return state;
};
