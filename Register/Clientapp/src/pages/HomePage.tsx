import React, { useState } from 'react';
import { Tabs, Tab, Box, Typography } from '@mui/material';
import PersonForm from '../components/PersonForm';
import PersonList from '../components/PersonList';
import PersonFormV2 from '../components/V2/PersonFormV2';
import PersonListV2 from '../components/V2/PersonListV2';
import { Person } from '../types/Person';
import { PersonV2 } from '../types/V2/PersonV2';

const HomePage: React.FC = () => {
  const [tab, setTab] = useState(0);

  // Estados para edição V1 e V2
  const [selectedPersonV1, setSelectedPersonV1] = useState<Person | undefined>();
  const [selectedPersonV2, setSelectedPersonV2] = useState<PersonV2 | undefined>();

  const handleChange = (_event: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };

  return (
    <Box p={2}>
      <Typography variant="h4" gutterBottom>
        Gestão de Pessoas
      </Typography>
      <Tabs value={tab} onChange={handleChange}>
        <Tab label="Versão 1" />
        <Tab label="Versão 2" />
      </Tabs>

      {/* Aba Versão 1 */}
      {tab === 0 && (
        <Box mt={3}>
          <PersonForm
            selectedPerson={selectedPersonV1}
            onSaved={() => setSelectedPersonV1(undefined)}
          />
          <PersonList onEdit={(person) => setSelectedPersonV1(person)} />
        </Box>
      )}

      {/* Aba Versão 2 */}
      {tab === 1 && (
        <Box mt={3}>
          <PersonFormV2
            selectedPerson={selectedPersonV2}
            onSaved={() => setSelectedPersonV2(undefined)}
          />
          <PersonListV2 onEdit={(person) => setSelectedPersonV2(person)} />
        </Box>
      )}
    </Box>
  );
};

export default HomePage;
