import { PersonBase } from './PersonBase';

export interface CreatePerson extends PersonBase {
  cpf: string;
}