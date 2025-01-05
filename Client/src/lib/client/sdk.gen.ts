// This file is auto-generated by @hey-api/openapi-ts

import { createClient, createConfig, type OptionsLegacyParser } from '@hey-api/client-fetch';
import type {
  GetApiFeedsError,
  GetApiFeedsResponse,
  PostApiFeedsSearchData,
  PostApiFeedsSearchError,
  PostApiFeedsSearchResponse,
  GetApiSubscriptionsError,
  GetApiSubscriptionsResponse,
  PostLogoutData,
  PostLogoutError,
  PostLogoutResponse,
  PostRegisterData,
  PostRegisterError,
  PostRegisterResponse,
  PostLoginData,
  PostLoginError,
  PostLoginResponse,
  PostRefreshData,
  PostRefreshError,
  PostRefreshResponse,
  CustomMapIdentityApiConfirmEmailData,
  CustomMapIdentityApiConfirmEmailError,
  CustomMapIdentityApiConfirmEmailResponse,
  PostResendConfirmationEmailData,
  PostResendConfirmationEmailError,
  PostResendConfirmationEmailResponse,
  PostForgotPasswordData,
  PostForgotPasswordError,
  PostForgotPasswordResponse,
  PostResetPasswordData,
  PostResetPasswordError,
  PostResetPasswordResponse,
  PostManage2FaData,
  PostManage2FaError,
  PostManage2FaResponse,
  GetManageInfoError,
  GetManageInfoResponse,
  PostManageInfoData,
  PostManageInfoError,
  PostManageInfoResponse,
  PostManageRolesData,
  PostManageRolesError,
  PostManageRolesResponse
} from './types.gen';

export const client = createClient(createConfig());

export const getApiFeeds = <ThrowOnError extends boolean = false>(
  options?: OptionsLegacyParser<unknown, ThrowOnError>
) => {
  return (options?.client ?? client).get<GetApiFeedsResponse, GetApiFeedsError, ThrowOnError>({
    ...options,
    url: '/api/Feeds'
  });
};

export const postApiFeedsSearch = <ThrowOnError extends boolean = false>(
  options?: OptionsLegacyParser<PostApiFeedsSearchData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostApiFeedsSearchResponse,
    PostApiFeedsSearchError,
    ThrowOnError
  >({
    ...options,
    url: '/api/Feeds/search'
  });
};

export const getApiSubscriptions = <ThrowOnError extends boolean = false>(
  options?: OptionsLegacyParser<unknown, ThrowOnError>
) => {
  return (options?.client ?? client).get<
    GetApiSubscriptionsResponse,
    GetApiSubscriptionsError,
    ThrowOnError
  >({
    ...options,
    url: '/api/Subscriptions'
  });
};

export const postLogout = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostLogoutData, ThrowOnError>
) => {
  return (options?.client ?? client).post<PostLogoutResponse, PostLogoutError, ThrowOnError>({
    ...options,
    url: '/logout'
  });
};

export const postRegister = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostRegisterData, ThrowOnError>
) => {
  return (options?.client ?? client).post<PostRegisterResponse, PostRegisterError, ThrowOnError>({
    ...options,
    url: '/register'
  });
};

export const postLogin = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostLoginData, ThrowOnError>
) => {
  return (options?.client ?? client).post<PostLoginResponse, PostLoginError, ThrowOnError>({
    ...options,
    url: '/login'
  });
};

export const postRefresh = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostRefreshData, ThrowOnError>
) => {
  return (options?.client ?? client).post<PostRefreshResponse, PostRefreshError, ThrowOnError>({
    ...options,
    url: '/refresh'
  });
};

export const customMapIdentityApiConfirmEmail = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<CustomMapIdentityApiConfirmEmailData, ThrowOnError>
) => {
  return (options?.client ?? client).get<
    CustomMapIdentityApiConfirmEmailResponse,
    CustomMapIdentityApiConfirmEmailError,
    ThrowOnError
  >({
    ...options,
    url: '/confirmEmail'
  });
};

export const postResendConfirmationEmail = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostResendConfirmationEmailData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostResendConfirmationEmailResponse,
    PostResendConfirmationEmailError,
    ThrowOnError
  >({
    ...options,
    url: '/resendConfirmationEmail'
  });
};

export const postForgotPassword = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostForgotPasswordData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostForgotPasswordResponse,
    PostForgotPasswordError,
    ThrowOnError
  >({
    ...options,
    url: '/forgotPassword'
  });
};

export const postResetPassword = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostResetPasswordData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostResetPasswordResponse,
    PostResetPasswordError,
    ThrowOnError
  >({
    ...options,
    url: '/resetPassword'
  });
};

export const postManage2Fa = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostManage2FaData, ThrowOnError>
) => {
  return (options?.client ?? client).post<PostManage2FaResponse, PostManage2FaError, ThrowOnError>({
    ...options,
    url: '/manage/2fa'
  });
};

export const getManageInfo = <ThrowOnError extends boolean = false>(
  options?: OptionsLegacyParser<unknown, ThrowOnError>
) => {
  return (options?.client ?? client).get<GetManageInfoResponse, GetManageInfoError, ThrowOnError>({
    ...options,
    url: '/manage/info'
  });
};

export const postManageInfo = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostManageInfoData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostManageInfoResponse,
    PostManageInfoError,
    ThrowOnError
  >({
    ...options,
    url: '/manage/info'
  });
};

export const postManageRoles = <ThrowOnError extends boolean = false>(
  options: OptionsLegacyParser<PostManageRolesData, ThrowOnError>
) => {
  return (options?.client ?? client).post<
    PostManageRolesResponse,
    PostManageRolesError,
    ThrowOnError
  >({
    ...options,
    url: '/manage/roles'
  });
};
