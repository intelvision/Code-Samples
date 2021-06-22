<div className="signUpFormWrapper authFormsWrapper">
			<Formik
				initialValues={initialSignUpFormValues}
				validationSchema={SignUpFormSchema}
				onSubmit={onSubmit}>
				{({values, errors, touched}) => (
					<Form>
						<Content className="formBody">
							<h3 className="formName">
								<b>Effettua la Registrazione</b>
							</h3>
							<div className="formGroup">
								<label htmlFor="email">
									Email<span className="error">*</span>
								</label>
								<Field
									className="field"
									id="email"
									name="email"
									placeholder="john@acme.com"
									type="email"
								/>
								{errors.email && touched.email ? (
									<div>
										<span className="error">{errors.email}</span>
									</div>
								) : null}
							</div>
							<div className="formGroup">
								<label htmlFor="password">
									Password<span className="required">*</span>
								</label>
								<Field
									className="field"
									id="password"
									type="password"
									name="password"
									placeholder=""
								/>
								{errors.password && touched.password ? (
									<div>
										<span className="error">{errors.password}</span>
									</div>
								) : null}
							</div>
							....and so on