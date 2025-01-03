// This file is auto-generated by @hey-api/openapi-ts

export type AccessTokenResponse = {
  readonly tokenType?: string | null;
  accessToken: string | null;
  expiresIn: number;
  refreshToken: string | null;
};

export type ApiResponse = {
  success?: boolean;
  message?: string | null;
  errors?: Array<string> | null;
  readonly hasErrors?: boolean;
};

export type AuthenticationResponse = {
  success?: boolean;
  message?: string | null;
  errors?: Array<string> | null;
  readonly hasErrors?: boolean;
  token?: string | null;
  expiration?: string;
};

export type Feed = {
  id?: string;
  title: string | null;
  description: string | null;
  webUrl: string | null;
  feedUrl: string | null;
  imageUrl?: string | null;
  language?: string | null;
  categories?: Array<string> | null;
  items?: Array<FeedItem> | null;
  searchVector?: Array<Lexeme> | null;
};

export type FeedItem = {
  id?: string;
  title: string | null;
  plainTextContent?: string | null;
  htmlContent?: string | null;
  url: string | null;
  summary?: string | null;
  imageUrl?: string | null;
  published?: string;
  author?: string | null;
  categories?: Array<string> | null;
  feedId?: string;
  feed?: Feed;
  searchVector?: Array<Lexeme> | null;
};

export type ForgotPasswordRequest = {
  email: string | null;
};

export type HttpValidationProblemDetails = {
  type?: string | null;
  title?: string | null;
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
  errors?: {
    [key: string]: Array<string>;
  } | null;
  [key: string]: (unknown | string | number) | undefined;
};

export type InfoRequest = {
  newEmail?: string | null;
  newPassword?: string | null;
  oldPassword?: string | null;
};

export type InfoResponse = {
  email: string | null;
  isEmailConfirmed: boolean;
};

export type ItemResponse = {
  id?: string;
  title: string | null;
  plainTextContent?: string | null;
  htmlContent?: string | null;
  url: string | null;
  summary?: string | null;
  imageUrl?: string | null;
  published?: string;
  author?: string | null;
  feedId?: string;
  categories?: Array<string> | null;
};

export type Lexeme = {
  text?: string | null;
  readonly count?: number;
};

export type LoginRequest = {
  email: string | null;
  password: string | null;
  twoFactorCode?: string | null;
  twoFactorRecoveryCode?: string | null;
};

export type LoginUserRequest = {
  email: string;
  password: string;
};

export type ProblemDetails = {
  type?: string | null;
  title?: string | null;
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
  [key: string]: (unknown | string | number) | undefined;
};

export type RefreshRequest = {
  refreshToken: string | null;
};

export type RegisterUserRequest = {
  name: string;
  email: string;
  password: string;
  passwordConfirmation: string;
};

export type RegistrationRequest = {
  name?: string | null;
  email: string | null;
  password: string | null;
};

export type ResendConfirmationEmailRequest = {
  email: string | null;
};

export type ResetPasswordRequest = {
  email: string | null;
  resetCode: string | null;
  newPassword: string | null;
};

export type SearchResponse = {
  count?: number;
  items?: Array<ItemResponse> | null;
};

export type TwoFactorRequest = {
  enable?: boolean | null;
  twoFactorCode?: string | null;
  resetSharedKey?: boolean;
  resetRecoveryCodes?: boolean;
  forgetMachine?: boolean;
};

export type TwoFactorResponse = {
  sharedKey: string | null;
  recoveryCodesLeft: number;
  recoveryCodes?: Array<string> | null;
  isTwoFactorEnabled: boolean;
  isMachineRemembered: boolean;
};

export type UserWithRolesResponse = {
  name: string | null;
  email: string | null;
  roles?: Array<string> | null;
};

export type UserWithRolesResponseApiResponse = {
  success?: boolean;
  message?: string | null;
  errors?: Array<string> | null;
  readonly hasErrors?: boolean;
  data?: UserWithRolesResponse;
};

export type Void = {
  [key: string]: unknown;
};

export type PostApiAccountRegisterData = {
  body?: RegisterUserRequest;
};

export type PostApiAccountRegisterResponse = ApiResponse;

export type PostApiAccountRegisterError = unknown;

export type PostApiAccountLoginData = {
  body?: LoginUserRequest;
};

export type PostApiAccountLoginResponse = AuthenticationResponse;

export type PostApiAccountLoginError = unknown;

export type GetApiAccountUserResponse = UserWithRolesResponseApiResponse;

export type GetApiAccountUserError = unknown;

export type GetApiFeedsResponse = Array<Feed>;

export type GetApiFeedsError = unknown;

export type PostApiFeedsSearchData = {
  query?: {
    searchTerm?: string;
  };
};

export type PostApiFeedsSearchResponse = SearchResponse;

export type PostApiFeedsSearchError = ProblemDetails;

export type PostLogoutData = {
  body: unknown;
};

export type PostLogoutResponse = Void;

export type PostLogoutError = unknown;

export type PostRegisterData = {
  body: RegistrationRequest;
};

export type PostRegisterResponse = unknown;

export type PostRegisterError = HttpValidationProblemDetails;

export type PostLoginData = {
  body: LoginRequest;
  query?: {
    useCookies?: boolean;
    useSessionCookies?: boolean;
  };
};

export type PostLoginResponse = AccessTokenResponse;

export type PostLoginError = unknown;

export type PostRefreshData = {
  body: RefreshRequest;
};

export type PostRefreshResponse = AccessTokenResponse;

export type PostRefreshError = unknown;

export type CustomMapIdentityApiConfirmEmailData = {
  query: {
    changedEmail?: string;
    code: string;
    userId: string;
  };
};

export type CustomMapIdentityApiConfirmEmailResponse = unknown;

export type CustomMapIdentityApiConfirmEmailError = unknown;

export type PostResendConfirmationEmailData = {
  body: ResendConfirmationEmailRequest;
};

export type PostResendConfirmationEmailResponse = unknown;

export type PostResendConfirmationEmailError = unknown;

export type PostForgotPasswordData = {
  body: ForgotPasswordRequest;
};

export type PostForgotPasswordResponse = unknown;

export type PostForgotPasswordError = HttpValidationProblemDetails;

export type PostResetPasswordData = {
  body: ResetPasswordRequest;
};

export type PostResetPasswordResponse = unknown;

export type PostResetPasswordError = HttpValidationProblemDetails;

export type PostManage2FaData = {
  body: TwoFactorRequest;
};

export type PostManage2FaResponse = TwoFactorResponse;

export type PostManage2FaError = HttpValidationProblemDetails | unknown;

export type GetManageInfoResponse = UserWithRolesResponse;

export type GetManageInfoError = HttpValidationProblemDetails | unknown;

export type PostManageInfoData = {
  body: InfoRequest;
};

export type PostManageInfoResponse = InfoResponse;

export type PostManageInfoError = HttpValidationProblemDetails | unknown;
