import { PersonV2Base } from './PersonV2Base';

export interface CreatePersonV2 extends PersonV2Base {
  cpf: string;
}