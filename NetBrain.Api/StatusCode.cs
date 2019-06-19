namespace NetBrain.Api
{
	public enum StatusCode
	{
		// Service Status Code HTTP Status Code Description
		// Success

		Success = 790200,
		Created = 790201,

		// Parameter Validation Failure
		NullParameter = 791000,
		InvalidParameter = 791001,
		InvalidValue = 791002,
		ParameterConflict = 791003,
		InvalidTenant = 791004,
		InvalidDomain = 791005,
		NonExistingData = 791006,
		DataConflict = 791007,
		UnsupportedFeature = 791008,
		NullOrInvalidValue = 791009,
		AttributeNotEditable = 792000,
		AttributeNotSupported = 792002,
		BuiltInAttributeDeletionError = 792003,
		FieldNameViolation = 792004,
		ValueOutOfRange = 792005,
		AlphabetStringConstraint = 792006,
		CharacterValidationFailed = 792007,
		InvalidAddress = 792008,
		InvalidType = 792009,
		TheMapIsNotExisting = 792010,
		UnknownSwitch = 792011,
		ZeroOrNegativeValue = 792030,
		NegativeValue = 792031,
		UnknownSourceGateway = 792040,
		SiteTransactionNotFound = 792100,
		SiteLocked = 792102,
		TryingToExecuteAnOperationThatIsInvalidForALeafSite = 792105,
		TryingToExecuteAnOperationThatIsInvalidForAContainerSite = 792110,

		// Server Exception
		UnexpectedError = 793000,
		InnerException = 793001,
		TaskError = 793011,

		// Routing Error
		NoRoutingResourceFoundForGivenUrl = 793404,
		TheMethodIsNotSupportedForGivenHttpRequest = 793405,

		// Task or Operation failure
		NoIpToDiscover = 794000,
		TheTaskIsAlreadyInRunningState = 794001,
		RunDisabledTask = 794002,
		TaskTypeIsNotScanned = 794003,
		TheTaskIsNotExisting = 794004,
		FailedToStartATask = 794005,
		IncompleteTask = 794007,
		FailedTask = 794008,
		CanceledTask = 794009,
		OperationConflict = 794010,
		OperationFailed = 794011,
		TaskTypeErrorNotABenchmarkTask = 794100,
		TaskTypeErrorNoADiscoveryTask = 794101,

		// Authentication Failure and Permission Error
		AuthenticationError = 795000,
		InsufficientPermissionsOnTenantOrDomain = 795001,
		NoPermissionToLogIntoTheSystem = 795002,
		InsufficientPermissions = 795003,
		TheEessionExpired = 795004,
		InvalidToken = 795005,
		RequestTimeout = 795006,
		ExceedsTenantAvailableNode = 795007,
		DoNotAllowResettingPasswords = 795008,
		DoNotAllowModifyingParameter = 795009,
		TheSessionDoesNotExist = 795010,
		ApiCallsExceededTheLimit = 795011,
		NoApiLicenseOrLicenseExpired = 795012,

		// External Approval for Change Management
		TheChangeManagementFailedToBindToATicket = 798800,
		TheChangeManagementWasAlreadyBoundToAnotherTicket = 798801,
		TheChangeManagementIsNotAllowedToBindToATicket = 798802,
		TheChangeManagementStateIsNotAllowedToBeChanged = 798803,
		FailedToUpdateTheChangeManagementState = 798804,
		LicenseMissingOrInvalid = 798805,
	}
}
