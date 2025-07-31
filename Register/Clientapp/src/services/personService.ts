import apiClient from '../api/api'; // Importa a instância configurada do Axios
import { Person } from '../types/Person';
import { CreatePerson } from '../types/CreatePerson';
import { UpdatePerson } from '../types/UpdatePerson';

const API_RESOURCE_URL = '/v1/persons'; // A base '/api' já está no apiClient

export const getPersons = async (): Promise<Person[]> => {
  try {
    const response = await apiClient.get<{ success: boolean; data: Person[] }>(API_RESOURCE_URL);
    return response.data.data ?? [];
  } catch (error) {
    console.error('Erro ao buscar pessoas:', error);
    return [];
  }
};

export const getPersonById = async (id: string): Promise<Person | null> => {
  try {
    const response = await apiClient.get<{ success: boolean; data: Person }>(`${API_RESOURCE_URL}/${id}`);
    return response.data.data ?? null;
  } catch (error) {
    console.error(`Erro ao buscar pessoa com ID ${id}:`, error);
    return null;
  }
};

export const createPerson = async (data: CreatePerson): Promise<Person | null> => {
  try {
    const response = await apiClient.post<{ success: boolean; data: Person }>(API_RESOURCE_URL, data);
    return response.data.data;
  } catch (error) {
    console.error('Erro ao criar pessoa:', error);
    return null;
  }
};

export const updatePerson = async (id: string, data: UpdatePerson): Promise<Person | null> => {
  try {
    const response = await apiClient.put<{ success: boolean; data: Person }>(`${API_RESOURCE_URL}/${id}`, data);
    return response.data.data;
  } catch (error) {
    console.error(`Erro ao atualizar pessoa com ID ${id}:`, error);
    return null;
  }
};

export const deletePerson = async (id: string): Promise<boolean> => {
  try {
    await apiClient.delete(`${API_RESOURCE_URL}/${id}`);
    return true;
  } catch (error) {
    console.error(`Erro ao deletar pessoa com ID ${id}:`, error);
    return false;
  }
};
