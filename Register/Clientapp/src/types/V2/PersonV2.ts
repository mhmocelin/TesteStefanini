export interface Address {
  street: string;
  number: string;
  neighborhood: string;
  city: string;
  state: string;
  country: string;
}

export interface PersonV2 {
  id: string;
  name: string;
  cpf: string;
  birthDate: string; // ISO string, ex: '2025-07-31T00:00:00Z'
  createdAt: string;
  updatedAt: string;
  gender?: string | null;
  email?: string | null;
  placeOfBirth?: string | null;
  nationality?: string | null;
  address: Address;  // obrigatório na v2
}