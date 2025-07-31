export interface Address {
  street: string;
  number: string;
  neighborhood: string;
  city: string;
  state: string;
  country: string;
}

export interface PersonV2Base {
  name: string;
  gender?: string | null;
  email?: string | null;
  birthDate: string; // ISO string
  placeOfBirth?: string | null;
  nationality?: string | null;
  address: Address; // obrigatório na v2
}