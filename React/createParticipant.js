export const createParticipant = (data) => {
    return (dispatch) => {
      dispatch(handleLoader(true));
      http
        .post('/participant/add', {...data.newParticipant, token: data.token})
        .then((response) => {
          if (response.status === 200) {
            dispatch(setIsParticipantActionTriggered(true))
            dispatch(addParticipant(response.data.participant))
            NotificationSuccess('Adding participant', 'Participant was successfully added');
            //history.push(`/payments`);
          }
        })
        .catch((e) => {
          NotificationError('Error while adding participant', e.data.error);
          console.log(e.data.error);
          dispatch(setError('createParticipant'));
        })
        .finally(() => {
          dispatch(handleLoader(false));
        });
    };
  };