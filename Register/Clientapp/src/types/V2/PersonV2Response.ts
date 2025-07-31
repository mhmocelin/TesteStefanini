import { PersonV2Base } from './PersonV2Base';

export interface PersonV2Response extends PersonV2Base {
  id: string;
  cpf: string;
  createdAt: string;
  updatedAt: string;
}