import { PersonBase } from './PersonBase';

export interface Person extends PersonBase {
  id: string;
  cpf: string;
  createdAt: string;
  updatedAt?: string;
}