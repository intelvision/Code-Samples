import {
    RESET_PARTICIPANTS, SET_IS_PARTICIPANT_ACTION_TRIGGERED,
    SET_PARTICIPANT,
    SET_PARTICIPANT_IDS,
    SET_PARTICIPANTS_COUNT,
    SET_SELECTED_PARTICIPANT_ID
} from "../actions/types";

const initialState = {
    participantsList: [],
    participantIds: [],
    selectedParticipantId: null,
    isParticipantActionTriggered: false
}

export const participantsReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_PARTICIPANT:
            return {
                ...state,
                participantsList: [...state.participantsList, action.payload.participant],
            }
        case SET_PARTICIPANT_IDS:
            return {
                ...state,
                participantIds: [...state.participantIds, action.payload.participant._id]
            }
        case RESET_PARTICIPANTS:
            return {
                ...state,
                participantsList: [],
                participantId: null
            }
        default:
            return state

    }
}