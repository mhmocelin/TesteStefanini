export interface LoginCredentials {
  username?: string;
  password?: string;
}

export interface AuthSuccessData {
  token: string;
  role: string;
}

export interface AuthResponse {
  success: boolean;
  data?: AuthSuccessData;
}

export interface AuthErrorResponse {
  success: boolean;
  errors: string[];
}