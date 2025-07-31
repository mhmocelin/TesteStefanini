import axios from 'axios';
import { LoginCredentials, AuthResponse, AuthErrorResponse } from '../types/Auth';

// A chamada de login não deve usar o interceptor que adiciona o token,
// por isso usamos o axios diretamente ou uma instância separada.
const AUTH_API_URL = '/api/v1/auth/login';

export const login = async (credentials: LoginCredentials): Promise<AuthResponse> => {
  try {
    const response = await axios.post<AuthResponse>(AUTH_API_URL, credentials);
    
    // Verifica se a resposta indica sucesso e se há um token
    if (response.data.success && response.data.data?.token) {
      const { token } = response.data.data;
      // Salva o token no localStorage para uso futuro nas requisições
      localStorage.setItem('authToken', token);
      return response.data;
    } else {
      // Se success for false ou não houver token, lança um erro com as mensagens
      const errorMessages = (response.data as unknown as AuthErrorResponse).errors || ["Erro desconhecido no login."];
      throw new Error(errorMessages.join(', '));
    }
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      // Captura erros da API (como o 500 que você mencionou)
      const errorData = error.response.data as AuthErrorResponse;
      const errorMessages = errorData.errors || ['Usuário ou senha inválidos'];
      throw new Error(errorMessages.join(', '));
    }
    // Lança outros erros (ex: de rede)
    throw new Error('Não foi possível conectar à API.');
  }
};

export const logout = (): void => {
  // Remove o token do localStorage ao fazer logout
  localStorage.removeItem('authToken');
};

// Função para verificar se existe um token (para checagem inicial)
export const isAuthenticated = (): boolean => {
  return !!localStorage.getItem('authToken');
};