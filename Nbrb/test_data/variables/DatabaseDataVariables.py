currencies_table_name = 'CURRENCIES'
dynamics_table_name = 'DYNAMICS'
column_names = {currencies_table_name: ['Cur_ID', 'Cur_ParentID', 'Cur_Code', 'Cur_Abbreviation',
                                        'Cur_Name', 'Cur_Name_Bel', 'Cur_Name_Eng',
                                        'Cur_QuotName', 'Cur_QuotName_Bel', 'Cur_QuotName_Eng',
                                        'Cur_NameMulti', 'Cur_Name_BelMulti', 'Cur_Name_EngMulti',
                                        'Cur_Scale', 'Cur_Periodicity', 'Cur_DateStart', 'Cur_DateEnd'],
                dynamics_table_name: ['Cur_ID', 'Date_cur', 'Cur_OfficialRate']}
column_types = {currencies_table_name: ['number(4)', 'number(4)', 'varchar2(5)', 'varchar2(5)',
                                        'varchar2(200)', 'varchar2(200)', 'varchar2(200)',
                                        'varchar2(200)', 'varchar2(200)', 'varchar2(200)',
                                        'varchar2(200)', 'varchar2(200)', 'varchar2(200)',
                                        'number(10)', 'number(1)', 'varchar2(20)', 'varchar2(20)'],
                dynamics_table_name: ['number(4)', 'Date', 'number(10,5)']}
