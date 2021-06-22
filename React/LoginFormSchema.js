const SignUpFormSchema = Yup.object().shape({
	email: Yup.string()
		.email('Email non valida')
		.required('Email è obbligatoria'),
	password: Yup.string()
		.min(8, 'la password deve avere minimo 8 caratteri')
		.required('la password è obbligatorio'),
	confirmPassword: Yup.string()
		.oneOf([Yup.ref('password'), null], "Does not match with password!")
		.min(8, 'la password deve avere minimo 8 caratteri')
		.required('la password è obbligatorio'),
	dontHaveTaxCode: Yup.boolean(),
    taxCode: Yup.string().when('dontHaveTaxCode', {
    		is: false,
    		then: Yup.string()
    			.required('codice fiscale è obbligatorio')
    			.min(16, 'codice fiscale deve avere minimo 16 caratteri'),
    	}),
		...