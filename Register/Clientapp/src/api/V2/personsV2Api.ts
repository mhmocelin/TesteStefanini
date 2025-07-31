import axios from 'axios';
import { PersonV2 } from '../../types/V2/PersonV2';
import { CreatePersonV2 } from '../../types/V2/CreatePersonV2';
import { UpdatePersonV2 } from '../../types/V2/UpdatePersonV2';

const API_URL_V2 = '/api/v2/persons';

export const getPersonsV2 = async (): Promise<PersonV2[]> => {
  try {
    const response = await axios.get<{ success: boolean; data: PersonV2[] }>(API_URL_V2);
    return response.data.data ?? [];
  } catch (error) {
    console.error('Erro ao buscar pessoas (v2):', error);
    return [];
  }
};

export const getPersonByIdV2 = async (id: string): Promise<PersonV2 | null> => {
  try {
    const response = await axios.get<{ success: boolean; data: PersonV2 }>(`${API_URL_V2}/${id}`);
    return response.data.data ?? null;
  } catch (error) {
    console.error(`Erro ao buscar pessoa (v2) com ID ${id}:`, error);
    return null;
  }
};

export const createPersonV2 = async (data: CreatePersonV2): Promise<PersonV2 | null> => {
  try {
    const response = await axios.post<{ success: boolean; data: PersonV2 }>(API_URL_V2, data);
    return response.data.data;
  } catch (error) {
    console.error('Erro ao criar pessoa (v2):', error);
    return null;
  }
};

export const updatePersonV2 = async (id: string, data: UpdatePersonV2): Promise<PersonV2 | null> => {
  try {
    const response = await axios.put<{ success: boolean; data: PersonV2 }>(`${API_URL_V2}/${id}`, data);
    return response.data.data;
  } catch (error) {
    console.error(`Erro ao atualizar pessoa (v2) com ID ${id}:`, error);
    return null;
  }
};

export const deletePersonV2 = async (id: string): Promise<boolean> => {
  try {
    await axios.delete(`${API_URL_V2}/${id}`);
    return true;
  } catch (error) {
    console.error(`Erro ao deletar pessoa (v2) com ID ${id}:`, error);
    return false;
  }
};
