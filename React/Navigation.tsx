<div className="app">
      {loader && <Loader />}
      <ReactNotification />
      <Router history={history}>
        <Template>
          <Switch>
            <Route exact path="/"> <Redirect to="/summer-camps" /> </Route>
            <Route exact path="/summer-camps" render={() => <SummerCamps />} />
            <ProtectedRoute
              exact
              path="/booking"
              render={() => <Booking currentUserParticipants={currentUserParticipants}
                token={token} currentUserId={currentUserId} />}
            />
            <Route
              path="/summer-camp-location/:_id"
              render={() => <SummerCampLocation />}
            />
            <ProtectedRoute path="/payments" render={() => <Payments />} />
            <Route path="/signin" render={() => <SignIn token={token} />} />
            <Route path="/signup" render={() => <SignUp />} />
            <ProtectedRoute
              exact
              path="/thanks"
              render={(props: any) => (
                <Thanks {...props} />
              )}/>
            <Route path="/email-was-sent" render={() => <EmailWasSent />} />
            <Route path="/password-reset-was-sent" render={() => <PasswordResetWasSent />} />
            <Route path="/email-verify" render={() => <EmailVerify />} />
            <Route path="/reset-password" render={() => <ResetPasswordForm />} />
          </Switch>
        </Template>
      </Router>
    </div>
