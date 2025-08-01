import React, { useEffect, useState, forwardRef, useImperativeHandle } from 'react';
import { PersonV2 } from '../../types/V2/PersonV2';
import { getPersonsV2, deletePersonV2 } from '../../services/personServiceV2';
import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';

interface PersonListV2Props {
  onEdit: (person: PersonV2) => void;
}

export interface PersonListV2Handles {
  loadPersonsV2: () => void;
}

const PersonListV2 = forwardRef<PersonListV2Handles, PersonListV2Props>(({ onEdit }, ref) => {
  const [persons, setPersons] = useState<PersonV2[]>([]);

  const loadPersonsV2 = async () => {
    const data = await getPersonsV2();
    setPersons(data);
  };

  useImperativeHandle(ref, () => ({
    loadPersonsV2,
  }));

  const handleDelete = async (id: string) => {
    await deletePersonV2(id);
    loadPersonsV2();
  };

  useEffect(() => {
    loadPersonsV2();
  }, []);

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Nacionalidade</TableCell>
            <TableCell>Data de Nascimento</TableCell>
            <TableCell>Endereço</TableCell>
            <TableCell align="center">#</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {persons.map((p) => (
            <TableRow key={p.id}>
              <TableCell>{p.name}</TableCell>
              <TableCell>{p.email}</TableCell>
              <TableCell>{p.nationality}</TableCell>
              <TableCell>{new Date(p.birthDate).toLocaleDateString()}</TableCell>
              <TableCell>
                {`${p.address.street}, ${p.address.number} - ${p.address.neighborhood}, ${p.address.city} - ${p.address.state}, ${p.address.country}`}
              </TableCell>
              <TableCell align="center">
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
});

export default PersonListV2;
