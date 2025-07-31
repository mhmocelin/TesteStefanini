import React, { useEffect, useState } from 'react';
import { Person } from '../types/Person';
import { getPersons, deletePerson } from '../api/personApi';
import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';

interface PersonListProps {
  onEdit: (person: Person) => void;
}

const PersonList: React.FC<PersonListProps> = ({ onEdit }) => {
  const [persons, setPersons] = useState<Person[]>([]);

  const loadPersons = async () => {
    const data = await getPersons();
    setPersons(data);
  };

  const handleDelete = async (id: string) => {
    await deletePerson(id);
    loadPersons();
  };

  useEffect(() => {
    loadPersons();
  }, []);

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>CPF</TableCell>
            <TableCell>Data de Nascimento</TableCell>
            <TableCell align="center">#</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {persons.map((p) => (
            <TableRow key={p.id}>
              <TableCell>{p.name}</TableCell>
              <TableCell>{p.email}</TableCell>
              <TableCell>{p.cpf}</TableCell>
              <TableCell>{new Date(p.birthDate).toLocaleDateString()}</TableCell>
              <TableCell>
                <Button variant="outlined" color="primary" onClick={() => onEdit(p)}>
                  Editar
                </Button>
                <Button
                  variant="outlined"
                  color="secondary"
                  onClick={() => handleDelete(p.id)}
                  style={{ marginLeft: 8 }}
                >
                  Deletar
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default PersonList;