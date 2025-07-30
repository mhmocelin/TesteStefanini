import React, { useState } from 'react';
import { Container, Typography } from '@mui/material';
import PersonList from '../components/PersonList';
import PersonForm from '../components/PersonForm';
import { Person } from '../types/Person';

const HomePage: React.FC = () => {
  const [selectedPerson, setSelectedPerson] = useState<Person | undefined>();
  const [refreshKey, setRefreshKey] = useState(0);

  const handleEdit = (person: Person) => {
    setSelectedPerson(person);
  };

  const handleSaved = () => {
    setSelectedPerson(undefined); // limpa seleção
    setRefreshKey((prev) => prev + 1); // força recarregar lista
  };

  return (
    <Container maxWidth="md">
      <Typography variant="h4" align="center" gutterBottom style={{ marginTop: 20 }}>
        Cadastro de Pessoas
      </Typography>

      <PersonForm selectedPerson={selectedPerson} onSaved={handleSaved} />

      {/* A key força o PersonList recarregar quando atualizamos */}
      <PersonList key={refreshKey} onEdit={handleEdit} />
    </Container>
  );
};

export default HomePage;
