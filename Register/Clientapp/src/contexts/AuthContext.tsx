import React, { createContext, useState, useContext, ReactNode, useEffect } from 'react';
import { login as apiLogin, logout as apiLogout, isAuthenticated as checkIsAuthenticated } from '../services/authService';
import { LoginCredentials, AuthResponse } from '../types/Auth';

// 1. Define a "forma" do contexto: o que ele irá fornecer aos componentes.
interface AuthContextType {
  isAuthenticated: boolean;
  login: (credentials: LoginCredentials) => Promise<AuthResponse>;
  logout: () => void;
  isLoading: boolean; // Adicionado para dar feedback de carregamento inicial
}

// 2. Cria o contexto com um valor padrão (geralmente null ou undefined).
// O '!' é um non-null assertion, indicando que o valor será fornecido pelo Provider.
const AuthContext = createContext<AuthContextType>(null!);

// 3. Cria um hook customizado para facilitar o uso do contexto.
// Em vez de usar useContext(AuthContext) em todo lugar, usaremos useAuth().
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth deve ser usado dentro de um AuthProvider');
  }
  return context;
};

// 4. Define as props do Provider, que deve aceitar componentes filhos.
interface AuthProviderProps {
  children: ReactNode;
}

// 5. Cria o componente Provider, que gerencia o estado e fornece o contexto.
export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true); // Começa como true

  // Efeito para verificar o token no localStorage quando o app carrega.
  useEffect(() => {
    const checkAuthStatus = () => {
      const authenticated = checkIsAuthenticated();
      setIsAuthenticated(authenticated);
      setIsLoading(false); // Finaliza o carregamento após a verificação
    };
    checkAuthStatus();
  }, []);

  // Função de login que chama a API e atualiza o estado.
  const login = async (credentials: LoginCredentials): Promise<AuthResponse> => {
    try {
      const response = await apiLogin(credentials);
      if (response.success) {
        setIsAuthenticated(true);
      }
      return response;
    } catch (error) {
      setIsAuthenticated(false);
      // O erro já é tratado no authService, aqui apenas o repassamos
      throw error;
    }
  };

  // Função de logout que chama a API (local) e atualiza o estado.
  const logout = () => {
    apiLogout();
    setIsAuthenticated(false);
  };

  // O valor que será compartilhado com os componentes filhos.
  const value = {
    isAuthenticated,
    login,
    logout,
    isLoading,
  };

  // Renderiza os componentes filhos dentro do Provider, dando a eles acesso ao 'value'.
  // O 'isLoading' previne que a aplicação "pisque" ou redirecione antes da verificação inicial.
  return (
    <AuthContext.Provider value={value}>
      {!isLoading && children}
    </AuthContext.Provider>
  );
};
