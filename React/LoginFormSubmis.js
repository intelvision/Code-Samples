const onSubmit = (e) => {

        const today = new Date()
        const maxYear = today.getFullYear() - maxAge
        const minYear = today.getFullYear() - minAge
        const currentMonth = today.getMonth() + 1
        const currentDay = today.getDate()

        if (dayjs(e.birthDate).isBetween(`${minYear}-${currentMonth}-${currentDay}`, `${maxYear}-${currentMonth}-${currentDay}`)) {

            if (state.participantInfo._id) {
                //UPDATE FLOW
                const new_participant = {...state.participantInfo}

                delete new_participant.dontHavePostalCode
                delete new_participant.idParticipant

                new_participant.gender === 'Maschio' ? new_participant.gender = 1 : new_participant.gender = 2
                new_participant.needBusTransport === 'Si' ? new_participant.needBusTransport = true : new_participant.needBusTransport = false
                new_participant.dontHavePostalCode && (new_participant.postalCode = '-')

                new_participant.userId = user._id

                dispatch(editParticipant({newParticipant: new_participant, token: auth.token}))

            } else {
                const new_participant = {...state.participantInfo}

                delete new_participant.dontHavePostalCode
                delete new_participant.idParticipant

                new_participant.gender === 'Maschio' ? new_participant.gender = 1 : new_participant.gender = 2
                new_participant.needBusTransport === 'Si' ? new_participant.needBusTransport = true : new_participant.needBusTransport = false
                new_participant.dontHavePostalCode && (new_participant.postalCode = '-')

                new_participant.userId = user._id

                for (let j = 0; j < currentUserParticipants.length; j++) {
                    if (areParticipantObjectsEqual(new_participant, currentUserParticipants[j])) {
                        return
                    }
                }

                dispatch(createParticipant({newParticipant: new_participant, token: auth.token}))
                //SAVE FLOW
            }
        } else {
            if (minAge > 0 && maxAge > 0) {
                NotificationError('Age is not acceptable', `Age should be from ${minAge} to ${maxAge}`);
            } else {
                NotificationError('Loading booking error', `Booking info hasn't loaded`);
            }
        }