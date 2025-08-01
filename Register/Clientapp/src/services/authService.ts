import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { LoginCredentials, AuthResponse, AuthErrorResponse } from '../types/Auth';

const AUTH_API_URL = '/api/v1/auth/login';

interface DecodedToken {
  exp: number;
}

const isTokenValid = (token: string | null): boolean => {
  if (!token) {
    return false;
  }

  try {
    const decodedToken: DecodedToken = jwtDecode(token);
    const currentTime = Date.now() / 1000;

    return decodedToken.exp > currentTime;
  } catch (error) {
    console.error("Falha ao decodificar o token:", error);
    return false;
  }
};

export const login = async (credentials: LoginCredentials): Promise<AuthResponse> => {
  try {
    const response = await axios.post<AuthResponse>(AUTH_API_URL, credentials);
    
    if (response.data.success && response.data.data?.token) {
      const { token } = response.data.data;
      localStorage.setItem('authToken', token);
      return response.data;
    } else {
      const errorMessages = (response.data as unknown as AuthErrorResponse).errors || ["Erro desconhecido no login."];
      throw new Error(errorMessages.join(', '));
    }
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      const errorData = error.response.data as AuthErrorResponse;
      const errorMessages = errorData.errors || ['Usuário ou senha inválidos'];
      throw new Error(errorMessages.join(', '));
    }
    throw new Error('Não foi possível conectar à API.');
  }
};

export const logout = (): void => {
  localStorage.removeItem('authToken');
};

export const isAuthenticated = (): boolean => {
  const token = localStorage.getItem('authToken');
  return isTokenValid(token);
};
