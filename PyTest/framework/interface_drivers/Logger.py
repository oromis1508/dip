import logging

class Logger:

    @staticmethod
    def add_log(log_file_name='out', class_name='root', log_level=3, log_type=logging.info, message=''):
        logger = logging.getLogger(class_name)
        logger.setLevel(logging.INFO)

        log_format = '%(levelname)s - %(message)s'
        if log_level >= 2:
            log_format = '%(name)s - {}'.format(log_format)
        if log_level == 3:
            log_format = '%(asctime)s - {}'.format(log_format)

        fh = logging.FileHandler('{}.log'.format(log_file_name))
        fh.setFormatter(log_format)
        log_type(message)
