import axios from 'axios';
import { Person } from '../types/Person';
import { CreatePerson } from '../types/CreatePerson';
import { UpdatePerson } from '../types/UpdatePerson';

const API_URL = '/api/v1/persons';

export const getPersons = async (): Promise<Person[]> => {
  try {
    const response = await axios.get<Person[] | null>(API_URL);
    return response.data ?? [];
  } catch (error) {
    console.error('Erro ao buscar pessoas:', error);
    return []; // Evita quebra na tela
  }
};

export const getPersonById = async (id: string): Promise<Person | null> => {
  try {
    const response = await axios.get<Person | null>(`${API_URL}/${id}`);
    return response.data ?? null;
  } catch (error) {
    console.error(`Erro ao buscar pessoa com ID ${id}:`, error);
    return null;
  }
};

export const createPerson = async (data: CreatePerson): Promise<Person | null> => {
  try {
    const response = await axios.post<Person>(API_URL, data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar pessoa:', error);
    return null;
  }
};

export const updatePerson = async (id: string, data: UpdatePerson): Promise<Person | null> => {
  try {
    const response = await axios.put<Person>(`${API_URL}/${id}`, data);
    return response.data;
  } catch (error) {
    console.error(`Erro ao atualizar pessoa com ID ${id}:`, error);
    return null;
  }
};

export const deletePerson = async (id: string): Promise<boolean> => {
  try {
    await axios.delete(`${API_URL}/${id}`);
    return true;
  } catch (error) {
    console.error(`Erro ao deletar pessoa com ID ${id}:`, error);
    return false;
  }
};